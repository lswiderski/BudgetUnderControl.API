﻿
namespace BudgetUnderControl.Common
{
    public interface IFileHelper
    {
        string GetLocalFilePath(string filename);
        void SaveText(string filename, string text);
        string LoadText(string filename);
        void SaveText(string filename, string[] lines);
        void SaveTextExternal(string filename, string[] lines);
        void SaveTextExternal(string filename, string text);
        string LoadTextExternal(string filename);
    }
}
