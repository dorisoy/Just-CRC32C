#include "library.h"
#include <immintrin.h>
#include <stdbool.h>
extern bool HasSSE42(){
    __builtin_cpu_init();
    return __builtin_cpu_supports("sse4.2");
}
extern unsigned long long CRC32_SSE42_Native_x64(const unsigned char *ptr, int ptr_length)
{
    unsigned long long crc = 0x00000000FFFFFFFF;
    int i = 0;
    int length = ptr_length;
    // Process the data in blocks of 32 bytes
    while (length >= 32)
    {
        crc = _mm_crc32_u64(crc, *((unsigned long long*)&ptr[i]));
        crc = _mm_crc32_u64(crc, *((unsigned long long*)&ptr[i+8]));
        crc = _mm_crc32_u64(crc, *((unsigned long long*)&ptr[i+16]));
        crc = _mm_crc32_u64(crc, *((unsigned long long*)&ptr[i+24]));
        i += 32;
        length -= 32;
    }
    remaining:
    if (length >= 8)
    {
        crc = _mm_crc32_u64(crc, *((unsigned long long*)&ptr[i]));
        i += 8;
        length -= 8;
        goto remaining;
    }
    if (length >= 4)
    {
        crc = _mm_crc32_u32((unsigned int)crc, *((unsigned int*)&ptr[i]));
        i += 4;
        length -= 4;
        goto remaining;
    }
    if (length >= 2)
    {
        crc = _mm_crc32_u16((unsigned int)crc, *((unsigned short*)&ptr[i]));
        i += 2;
        length -= 2;
        goto remaining;
    }
    if (length >= 1)
    {
        crc = _mm_crc32_u8((unsigned int)crc, *(&ptr[i]));
        i += 1;
        length -= 1;
        goto remaining;
    }
    return ~crc;
}
extern unsigned int CRC32_SSE42_Native_x32(const unsigned char *ptr, int ptr_length)
{
    unsigned int crc = 0xFFFFFFFF;
    int i = 0;
    int length = ptr_length;
    // Process the data in blocks of 16 bytes
    while (length >= 16)
    {
        crc = _mm_crc32_u32(crc, *((unsigned int*)&ptr[i]));
        crc = _mm_crc32_u32(crc, *((unsigned int*)&ptr[i+4]));
        crc = _mm_crc32_u32(crc, *((unsigned int*)&ptr[i+8]));
        crc = _mm_crc32_u32(crc, *((unsigned int*)&ptr[i+12]));
        i += 16;
        length -= 16;
    }
    remaining:
    if (length >= 4)
    {
        crc = _mm_crc32_u32(crc, *((unsigned int*)&ptr[i]));
        i += 4;
        length -= 4;
        goto remaining;
    }
    if (length >= 2)
    {
        crc = _mm_crc32_u16(crc, *((unsigned short*)&ptr[i]));
        i += 2;
        length -= 2;
        goto remaining;
    }
    if (length >= 1)
    {
        crc = _mm_crc32_u8(crc, ptr[i]);
        ++i;
        --length;
        goto remaining;
    }
    return ~crc;
}