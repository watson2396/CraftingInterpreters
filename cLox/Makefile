CC = clang
CFLAGS = -Wall -Wextra -g -std=c11
TARGET = output/clox

SRC_DIR = src
BUILD_DIR = output/build

HDRS = $(wildcard $(SRC_DIR)/*.h)
SRCS = $(wildcard $(SRC_DIR)/*.c)
OBJS = $(patsubst $(SRC_DIR)/%.c, $(BUILD_DIR)/%.o, $(SRCS))

$(TARGET): $(OBJS)
	$(CC) $(CFLAGS) -o $(TARGET) $(OBJS)

$(BUILD_DIR)/%.o: $(SRC_DIR)/%.c | $(BUILD_DIR)
	$(CC) $(CFLAGS) -c $< -o $@

$(BUILD_DIR):
	mddir -p (BUILD_DIR)

.PHONY: clean
clean:
	rm -rf $(BUILD_DIR) 
	rm -f $(TARGET)