using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.IO;
using System.Diagnostics;

namespace TokenAssist
{
    public enum EntryType
    {
        TYPE_POWER,
        TYPE_ITEM,
        TYPE_FEAT,
    }

    public class CompendiumCache
    {
        const int SOURCE_NET = 1 << 0;
        const int SOURCE_CACHE = 1 << 1;
        const int SOURCE_CHAR = 1 << 2;
        const int SOURCE_CLASS = 1 << 3;
        const int SOURCE_RACE = 1 << 4;

        Character mActiveChar = null;
        int mAvailableSources = 0;

        string mPowersDir = null;
        string mItemsDir = null;
        string mFeatsDir = null;
        string mCharsDir = null;
        
        public CompendiumCache()
        {
            mAvailableSources = SOURCE_NET | SOURCE_CACHE | SOURCE_CHAR | SOURCE_CLASS | SOURCE_RACE;
            
            try
            {
                string mCacheBase = Path.Combine(Dropbox.Folder, "D&D");
                if (!Directory.Exists(mCacheBase))
                    throw new Exception("We don't have a share to cache at");

                mCacheBase = Path.Combine(mCacheBase, "TokenAssist");
                if (!Directory.Exists(mCacheBase))
                    Directory.CreateDirectory(mCacheBase);

                mPowersDir = Path.Combine(mCacheBase, "Powers");
                if (!Directory.Exists(mPowersDir))
                    Directory.CreateDirectory(mPowersDir);

                mItemsDir = Path.Combine(mCacheBase, "Items");
                if (!Directory.Exists(mItemsDir))
                    Directory.CreateDirectory(mItemsDir);
                
                mFeatsDir = Path.Combine(mCacheBase, "Feats");
                if (!Directory.Exists(mFeatsDir))
                    Directory.CreateDirectory(mFeatsDir);

                mCharsDir = Path.Combine(mCacheBase, "Characters");
                if (!Directory.Exists(mCharsDir))
                    Directory.CreateDirectory(mCharsDir);
            }
            catch (Exception)
            {
                mAvailableSources = SOURCE_NET;
            }
        }

        public Character ActiveCharacter
        {
            set
            {
                mActiveChar = value;
            }
            get
            {
                return mActiveChar;
            }
        }

        /// <param name="id">The id to try and find</param>
        /// <returns>A string with the entry</returns>
        public string Get(EntryType type, string url)
        {
            string id = IDFromUrl(url);
            string result = null;

            Debug.WriteLine("CompendiumCache::Get " + type.ToString() + "," + url + "," + id);

            // decide our paths
            string cacheDir = mPowersDir;
            switch (type)
            {
                case EntryType.TYPE_POWER:
                    cacheDir = mPowersDir;
                    break;
                case EntryType.TYPE_ITEM:
                    cacheDir = mItemsDir;
                    break;
                case EntryType.TYPE_FEAT:
                    cacheDir = mFeatsDir;
                    break;
            }

            // Check the per character cache
            if ((SourceEnabled(SOURCE_CHAR)) && (result == null) && (type == EntryType.TYPE_POWER) && (mActiveChar != null))
            {
                string filename = null;
                if (mActiveChar.Name != null)
                    filename = Path.Combine(mCharsDir, mActiveChar.Name + @"\" + id + ".html");

                if (File.Exists(filename))
                {
                    try
                    {
                        using (StreamReader reader = new StreamReader(filename))
                        {
                            result = reader.ReadToEnd();
                            Debug.WriteLine("     from character cache");
                        }
                    }
                    catch (Exception)
                    {
                    }
                }
            }

            // Check the global cache
            string cacheName = Path.Combine(cacheDir, id + ".html");
            if ((SourceEnabled(SOURCE_CACHE)) && (result == null) && (id != null) && (File.Exists(cacheName)))
            {
                try
                {
                    using (StreamReader reader = new StreamReader(cacheName))
                    {
                        result = reader.ReadToEnd();
                        Debug.WriteLine("     from global cache");
                    }
                }
                catch (Exception)
                {
                }
            }
            

            // no answer yet?  Consult the compendium
            if ((SourceEnabled(SOURCE_NET)) && (result == null))
            {
                result = CompendiumUtilities.GetUrl(url);

                // if it came down to asking the compendium and we have an id to use, go store it in the cache
                if (id != null)
                {
                    using (StreamWriter writer = new StreamWriter(cacheName))
                    {
                        writer.Write(result);
                        Debug.WriteLine("     from compendium");
                    }
                }
            }

            return result;
        }


        /// <summary>
        /// Given a url, return the ID
        /// </summary>
        /// <returns>The ID in string form, if we couldn't find it returns null</returns>
        public string IDFromUrl(string url)
        {
            Regex pattern = new Regex(@"\?f?id=(\d+)");
            Match match = pattern.Match(url);
            if (match.Success)
                return match.Groups[1].ToString();
            else
                return null;
        }


        private bool SourceEnabled(int cacheType)
        {
            return ((mAvailableSources & cacheType) != 0);
        }

        public int CacheSettings
        {
            set
            {
                mAvailableSources = value;
            }
            get
            {
                return mAvailableSources;
            }
        }
    }
};
