; Процедуры
testp proc
   inc cx
   dec dx
   call testb
testp endp

testb proc
   mov bp, cx
   mov sp, dx
testb endp

mov cx, 20
mov dx, 30
call testp
mov ax, cx
mov bx, dx
