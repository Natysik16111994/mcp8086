; Деление через вычитание
mov ax, 200
mov bx, 100
s:
inc cx
sub ax, bx
jnz s
