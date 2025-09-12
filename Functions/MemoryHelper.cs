using System;
using System.Collections.Generic;

// Memory Helper
// 内存管理
// Version: 2025-9-12 18:44
// Author: isHuaMouRen

public class MemoryHelper
{
    private static List<byte[]> memoryBlocks = new List<byte[]>();

    /// <summary>
    /// 无意义的占用内存，程序结束后释放
    /// </summary>
    /// <param name="megaBytes">占用的大小，单位MB</param>
    /// <exception cref="ArgumentException">占用的内存必须大于0</exception>
    public static void Allocate(int megaBytes)
    {
        if (megaBytes <= 0)
            throw new ArgumentException("必须大于0");

        int bytes = megaBytes * 1024 * 1024;

        // 分配并保存引用
        byte[] buffer = new byte[bytes];

        // 避免被优化，填充数据
        for (int i = 0; i < buffer.Length; i += 4096)
        {
            buffer[i] = 1;
        }

        memoryBlocks.Add(buffer); // 保存到列表，防止被 GC 回收
        Console.WriteLine($"已分配 {megaBytes} MB，总占用 {GetTotalMemoryMB()} MB");
    }

    /// <summary>
    /// 获取当前占用内存
    /// </summary>
    /// <returns>占用的内存，单位MB</returns>
    public static long GetTotalMemoryMB()
    {
        long total = 0;
        foreach (var block in memoryBlocks)
        {
            total += block.Length;
        }
        return total / 1024 / 1024;
    }
}
