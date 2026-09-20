#ifndef clox_vm_h
#define clox_vm_h

#include "chunk.h"
#include "value.h"

#define STACK_MAX 256

typedef struct
{
    Chunk* chunk;
    uint8_t* ip; // points to instruction about to be executed
    Value stack[STACK_MAX];

    /*
    * pointer points at the array element just past the element 
    * containing the top value on the stack
    *
    *  0 1 2 
    * [ | | ]
    *  ^ - empty stack
    *
    *  0 1 2 
    * [c| | ]
    *    ^ - stack with 1 element
    *
    */ 
    Value* stackTop; 
} VM;

typedef enum {
    INTERPRET_OK,
    INTERPRET_COMPILE_ERROR,
    INTERPRET_RUNTIME_ERROR,
} InterpretResult;

void initVM();
void freeVM();
InterpretResult interpret(Chunk* chunk);
void push(Value value);
Value pop();

#endif