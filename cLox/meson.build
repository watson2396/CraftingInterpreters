project(
  'clox',
  'c',
  meson_version : '>= 1.3.0',
  version : '0.1',
  default_options : ['warning_level=3'],
)

incdir = include_directories('./src')

dependencies = [
]

exe = executable(
  'clox',
  './src/main.c',
  dependencies : dependencies,
  include_directories: incdir,
  install : true,
)

test('basic', exe)
