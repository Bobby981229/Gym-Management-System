using System;
using System.Runtime.InteropServices;

namespace Gym_Management_System.Utils
{
    public class MemoryUtil
    {
        /// <summary>
        /// Apply for memory
        /// </summary>
        /// <param name="len">Memory length (unit: bytes)</param>
        /// <returns>First memory address</returns>
        public static IntPtr Malloc(int len)
        {
            return Marshal.AllocHGlobal(len);
        }

        /// <summary>
        /// Free PTR managed memory
        /// </summary>
        /// <param name="ptr">Hosting a pointer</param>
        public static void Free(IntPtr ptr)
        {
            Marshal.FreeHGlobal(ptr);
        }

        /// <summary>
        /// Copies the contents of the byte array into managed memory
        /// </summary>
        /// <param name="source">metadata</param>
        /// <param name="startIndex">Metadata copy start location</param>
        /// <param name="destination">Managed memory</param>
        /// <param name="length">Copy the length</param>
        public static void Copy(byte[] source, int startIndex, IntPtr destination, int length)
        {
            Marshal.Copy(source, startIndex, destination, length);
        }

        /// <summary>
        /// Copies the contents of managed memory into a byte array
        /// </summary>
        /// <param name="source">Managed memory</param>
        /// <param name="destination">Target byte array</param>
        /// <param name="startIndex">Copy start position</param>
        /// <param name="length">Copy the length</param>
        public static void Copy(IntPtr source, byte[] destination, int startIndex, int length)
        {
            Marshal.Copy(source, destination, startIndex, length);
        }

        /// <summary>
        /// Converts PTR managed memory into a struct object
        /// </summary>
        /// <typeparam name="T">Generics</typeparam>
        /// <param name="ptr">Hosting a pointer</param>
        /// <returns>The transformed object</returns>
        public static T PtrToStructure<T>(IntPtr ptr)
        {
            return Marshal.PtrToStructure<T>(ptr);
        }

        /// <summary>
        /// Copies the struct object into PTR managed memory
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        /// <param name="ptr"></param>
        public static void StructureToPtr<T>(T t,IntPtr ptr) {
            Marshal.StructureToPtr(t,ptr,false);
        }

        /// <summary>
        /// Gets the size of the type
        /// </summary>
        /// <typeparam name="T">Genericity</typeparam>
        /// <returns>Size of type</returns>
        public static int SizeOf<T>()
        {
            return Marshal.SizeOf<T>();
        }
    }
}
