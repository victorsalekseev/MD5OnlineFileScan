using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Diagnostics;
using System.IO;

namespace Netcode.Common
{
    public class ExFileInfo
    {
        public ExFileInfo()
        {
        }

        public delegate void OnMakeError(string Error);
        public event OnMakeError MakeError;

        public Hashtable LoadFInfo(string hash, string path, bool is_accept_file, DateTime CheckedDate)
        {
            Hashtable ht = new Hashtable(50);
            try
            {
                FileVersionInfo fvi = FileVersionInfo.GetVersionInfo(path);
                FileInfo fi = new FileInfo(path);

                ht.Add("isChecked", is_accept_file);
                ht.Add("CheckedDate", CheckedDate);

                if (fi != null)
                {
                    ht.Add("ShortName", fi.Name);
                    ht.Add("CreationTime", fi.CreationTime);
                    ht.Add("CreationTimeUtc", fi.CreationTimeUtc);
                    ht.Add("Directory", fi.Directory);
                    ht.Add("DirectoryName", fi.DirectoryName);
                    ht.Add("Extension", fi.Extension);
                    ht.Add("FullName", fi.FullName);
                    ht.Add("IsReadOnly", fi.IsReadOnly);
                    ht.Add("LastAccessTime", fi.LastAccessTime);
                    ht.Add("LastAccessTimeUtc", fi.LastAccessTimeUtc);
                    ht.Add("LastWriteTime", fi.LastWriteTime);
                    ht.Add("LastWriteTimeUtc", fi.LastWriteTimeUtc);
                    ht.Add("Length", fi.Length);
                    ht.Add("Attributes", fi.Attributes.ToString());
                }

                ht.Add("Hash", hash);

                if (fvi != null)
                {
                    ht.Add("Comments", fvi.Comments);
                    ht.Add("CompanyName", fvi.CompanyName);
                    ht.Add("FileBuildPart", fvi.FileBuildPart);
                    ht.Add("FileDescription", fvi.FileDescription);
                    ht.Add("FileMajorPart", fvi.FileMajorPart);
                    ht.Add("FileMinorPart", fvi.FileMinorPart);
                    ht.Add("FileName", fvi.FileName);
                    ht.Add("FilePrivatePart", fvi.FilePrivatePart);
                    ht.Add("FileVersion", fvi.FileVersion);
                    ht.Add("InternalName", fvi.InternalName);
                    ht.Add("IsDebug", fvi.IsDebug);
                    ht.Add("IsPatched", fvi.IsPatched);
                    ht.Add("IsPreRelease", fvi.IsPreRelease);
                    ht.Add("IsPrivateBuild", fvi.IsPrivateBuild);
                    ht.Add("IsSpecialBuild", fvi.IsSpecialBuild);
                    ht.Add("Language", fvi.Language);
                    ht.Add("LegalCopyright", fvi.LegalCopyright);
                    ht.Add("LegalTrademarks", fvi.LegalTrademarks);
                    ht.Add("OriginalFilename", fvi.OriginalFilename);
                    ht.Add("PrivateBuild", fvi.PrivateBuild);
                    ht.Add("ProductBuildPart", fvi.ProductBuildPart);
                    ht.Add("ProductMajorPart", fvi.ProductMajorPart);
                    ht.Add("ProductMinorPart", fvi.ProductMinorPart);
                    ht.Add("ProductName", fvi.ProductName);
                    ht.Add("ProductPrivatePart", fvi.ProductPrivatePart);
                    ht.Add("ProductVersion", fvi.ProductVersion);
                    ht.Add("SpecialBuild", fvi.SpecialBuild);
                }
            }
            catch (Exception ex)
            {
                if (MakeError != null)
                {
                    MakeError.Invoke(ex.Message);
                }
            }
            return ht;
        }
    }
}
