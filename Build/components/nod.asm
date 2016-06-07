; НОД
mov ax, 240
mov bx, 150

; - - -
compare:
cmp ax, bx
jc b_more_a
jmp start

b_more_a:
mov cx, ax
mov ax, bx
mov bx, cx

start:
;mov cx, ax
div bx
mov ax, dx
cmp ax, 0
jne compare

; НОД в BX

