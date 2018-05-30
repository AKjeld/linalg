.PHONY: clean

FILES=*.cs Core/*.cs

build:	ProjectC.csproj
	msbuild ProjectC.csproj

run:
	mono ./bin/Debug/ProjectC.exe

clean:
	rm -rf bin/ obj/
