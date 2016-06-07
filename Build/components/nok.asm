; НОК
mov ax, 240
mov bx, 150
; - - -
mov cs, ax
mov ds, bx
mul bx
mov es, ax

; максимум из A, B
cmp cs, ds
jc b_more_a
jmp start

b_more_a:
mov ax, cs
mov cs, ds
mov ds, ax

start:
mov cx, cs
dec cx

loop:
inc cx
mov ax, cx
div cs
cmp ah, 0
jne loop
mov ax, cx
div ds
cmp ah, 0
jne loop

; --- НОК в cx --
