using BenchmarkDotNet.Attributes;
using Perfolizer.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace AoC_Day_2.src
{
    [MemoryDiagnoser]
    public class AoCDay7
    {
        public class FileObject
        {
            public FileObject parent;
            public bool dir;
            public uint size;
            public string name;
            public Dictionary<string, FileObject> contents;

            //new directory
            public FileObject(FileObject parentNode,string filename, Dictionary<string, FileObject> Contains)
            {
                parent = parentNode; //path to parent directory, "ROOT" for root
                dir = true; //false for file, true for directory
                contents = Contains; //Dictionary of contents inside this folder if a folder
                name = filename;
             }
            public FileObject(FileObject parentNode,string filename, uint fileSize)
            {
                parent = parentNode; //path to parent directory, "ROOT" for root
                dir = false; //false for file, true for directory
                size = fileSize; //size of folder or file
                name = filename;
            }
            public FileObject()
            {

            }

        }
        private readonly static string inputPath = "C:\\Users\\kaist\\source\\repos\\AoC Day 2\\input\\day7input.txt";
        string samplePath = "C:\\Users\\kaist\\source\\repos\\AoC Day 2\\input\\day7sample.txt";
        string[] RawInput = File.ReadAllLines(inputPath);
        public int FileReadIndex = 1;
        public string pwd = "/";
        public FileObject rootFile = new FileObject(null, "ROOT", new Dictionary<string, FileObject>());
        SortedSet<uint> sizes = new SortedSet<uint>();
       
        public void GetStarted()
        {
            traverseStructure(rootFile);
            addFileSizes(rootFile);
            findFolderToDelete(30000000 - (70000000 - rootFile.size));
        }
        public void findFolderToDelete(uint sizeNeeded)
        {
            uint previous = 0;
            foreach (uint size in sizes.Reverse())
            {
                if (size > sizeNeeded)
                {
                    previous = size;
                    continue;
                }
                else
                {
                    Console.WriteLine(previous);
                    break;
                }
                    

            }
        }
        public void addFileSizes(FileObject FileToCheck)
        {
            FileObject currentFile = FileToCheck;
            //if current node is a director, branch out to everything in it, then add current file sizes to parent folder size
            if (currentFile.dir)
            {
                foreach (FileObject file in currentFile.contents.Values)
                    addFileSizes(file);
                if(currentFile.name!="ROOT")
                    currentFile.parent.size+=currentFile.size; 
            }
            //otherwise,base case for recursion is finding a file so add the file size to the parent node size 
            else
            {
                currentFile.parent.size += currentFile.size;
            }
        }
        public void traverseStructure(FileObject CurrentFolderObject)
        {
            FileObject CurrentFolder = CurrentFolderObject;
            do
            {
                string[] command = RawInput[FileReadIndex++].Split(' ');
                //check if it's a command
                if (Char.Parse(command[0]) == '$')
                {
                    //if so, check if change directory
                    if (command[1] == "cd")
                    {
                        //go up a level
                        if (command[2] == "..")
                        {
                            traverseStructure(CurrentFolder.parent);
                        }
                        else
                        {
                            CurrentFolder = CurrentFolder.contents[command[2]];
                        }
                    }
                    //if command is ls
                    else if (command[1] == "ls")
                    {
                        CurrentFolder = ParseCurrentLevel(CurrentFolder);
                    }

                }
            }while(FileReadIndex < RawInput.Length);
        }
        public FileObject ParseCurrentLevel(FileObject CurrentFolder)
        {
            FileObject workingFolder = CurrentFolder;
            do
            {
                string[] nextLine = RawInput[FileReadIndex++].Split(" ");
                if (nextLine[0]=="dir")
                {
                    //we need to add a directory to current working directory
                    //traverse File Structure to current working directory
                    Dictionary<string, FileObject> newFolderContents = new Dictionary<string, FileObject>();
                    FileObject newFolder = new FileObject();
                    newFolder.parent = workingFolder;
                    newFolder.contents = newFolderContents;
                    newFolder.name = nextLine[1];
                    newFolder.dir = true;
                    workingFolder.contents.TryAdd(nextLine[1], newFolder);
                }    
                else if (uint.TryParse(nextLine[0],out uint size))
                {
                    FileObject newFile = new FileObject(CurrentFolder, nextLine[1], size);
                    
                    workingFolder.contents.TryAdd(nextLine[1], newFile);
                }
                else if (nextLine[0]=="$")
                {
                    FileReadIndex--;
                    return workingFolder; }
            } while (FileReadIndex < RawInput.Length);
            return CurrentFolder;
        }
    }
}
