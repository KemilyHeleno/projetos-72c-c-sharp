/* OBESO.C � informa se uma pessoa est� ou n�o obesa */
#include <stdio.h>
#include <conio.h>
#include <math.h>
#define LIMITE 30
main() 
{
	float peso, altura, imc;
	clrscr();
	printf(�\n Qual o seu peso e altura? �);
	scanf(�%f %f�, &peso, &altura);
	imc = peso/pow(altura,2);
	printf(�\n Seu i.m.c. � %.1f�, imc);
	if( imc <= LIMITE ) printf(�\n Voc� n�o est� obeso!�);
	else printf(�\n Voc� est� obeso!�);
	getch();
}
