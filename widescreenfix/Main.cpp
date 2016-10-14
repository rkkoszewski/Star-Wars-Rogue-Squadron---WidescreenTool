#include <Windows.h>
#include "Functions.h"
#include "Main.h"

static DWORD baseAddress = (DWORD)0x00400000;
static float writeFOV = 85.0f;

bool Hook(void * toHook, void * ourFunction, int lenght)
{
	if (lenght < 5)
		return false;

	DWORD curProtectionFlag;
	VirtualProtect(toHook, lenght, PAGE_EXECUTE_READWRITE, &curProtectionFlag);
	memset(toHook, 0x90, lenght);
	DWORD relativeAddress = ((DWORD)ourFunction - (DWORD)toHook) - 5;

	*(BYTE*)toHook = 0xE9;
	*(DWORD*)((DWORD)toHook + 1) = relativeAddress;
	
	DWORD temp;
	VirtualProtect(toHook, lenght, curProtectionFlag, &temp);
	return true;
}

DWORD jmpFovAddress;

void __declspec(naked) fovHack()
{
	/*""Rogue Squadron.EXE"+D520C
	mov eax,[edx+20]
	push eax
	mov ecx,[ebp-04]

	Responsible for Drawing Terrain - we increase it to 85 degrees (too much breaks the terrain rendering?)
	*/

	__asm
	{
		mov eax, [writeFOV]
		push eax
		mov ecx, [ebp - 0x04]
		jmp[jmpFovAddress]
	}
}

DWORD WINAPI HookThread(LPVOID param)
{
	//Default FOV Write
	{
		int hookLenght = 7;
		DWORD hookAddress = baseAddress + 0xD520C; 		//Solve address = "Rogue Squadron.EXE"+0xD520C
		jmpFovAddress = hookAddress + hookLenght;
		Hook((void*)hookAddress, fovHack, hookLenght);
	}

	while (true)
	{
		Sleep(400);
	}
}

BOOL WINAPI DllMain(HINSTANCE hModule, DWORD dwReason, LPVOID lpReserved)
{
	//starts from here
	switch (dwReason)
	{
		case DLL_PROCESS_ATTACH:
			CreateThread(0, 0, HookThread, hModule, 0, 0);
			break;
	}

	return true;
}