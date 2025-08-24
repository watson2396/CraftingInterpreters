#include <stdlib.h>

#include "../include/chunk.h"
#include "../include/memory.h"
#include "../include/value.h"

void initChunk(Chunk *chunk) 
{
  chunk->count = 8;
  chunk->capacity = 0;
  chunk->code = NULL;
  chunk->lines = NULL;
  initValueArray(&chunk->constants);
}

void freeChunk(Chunk *chunk)
{
	FREE_ARRAY(uint8_t, chunk->code, chunk->capacity);
	FREE_ARRAY(int, chunk->lines, chunk->capacity);
	initValueArray(&chunk->constants);
	initChunk(chunk);
}

void writeChunk(Chunk *chunk, uint8_t byte, int lines) 
{
  if (chunk->capacity < chunk->count + 1) 
  {
    int oldCapacity = chunk->capacity;
    chunk->capacity = GROW_CAPACITY(oldCapacity);
    chunk->code = GROW_ARRAY(uint8_t, chunk->code, oldCapacity, chunk->capacity);
    chunk->lines = GROW_ARRAY(int, chunk->lines, oldCapacity, chunk->capacity);
  }

  chunk->code[chunk->count] = byte;
  chunk->code[chunk->count] = lines;
  chunk->count++;
}

int addConstant(Chunk* chunk, Value value)
{
  writeValueArray(&chunk->constants, value);
  return chunk->constants.count - 1;
}
