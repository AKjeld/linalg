.PHONY: clean

FILES=*.cs Core/*.cs

build:	ProjectB.csproj
	msbuild ProjectB.csproj

run:
	mono ./bin/Debug/ProjectB.exe

clean:
	rm -rf bin/ obj/
