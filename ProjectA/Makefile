.PHONY: clean

FILES=*.cs Core/*.cs

build:	ProjectA.csproj
	msbuild ProjectA.csproj

run:
	mono ./bin/Debug/ProjectA.exe

clean:
	rm -rf bin/ obj/
