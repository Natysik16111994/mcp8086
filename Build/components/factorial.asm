; Факториал
mov cx, 5

; - - -
mov ax, 1
start:
	mov bx, ax
	mov ax, cx
	mul bx
loop start
