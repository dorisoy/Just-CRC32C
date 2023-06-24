#ifndef JUSTCRC32_NATIVE_LIBRARY_H
#define JUSTCRC32_NATIVE_LIBRARY_H
#include <stdbool.h>
extern unsigned long long CRC32_SSE42_Native_x64(const unsigned char *ptr, int ptr_length);
extern unsigned int CRC32_SSE42_Native_x32(const unsigned char *ptr, int ptr_length);
extern bool HasSSE42();
#endif //JUSTCRC32_NATIVE_LIBRARY_H
