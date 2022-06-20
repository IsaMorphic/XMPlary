// See https://aka.ms/new-console-template for more information
using XMPlary;

using (var stream = File.OpenRead(args[0]))
using (var xmFile = new XMFile(stream))
using (var objFile = File.CreateText(args[1]))
{
    Console.WriteLine("Converting XM file to OBJ file...");
    xmFile.WriteToOBJ(objFile);
}

Console.WriteLine($"Conversion complete. Output is at {args[1]}");
