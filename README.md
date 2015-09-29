# Matrix-Calculator


This is an educational project realized by students of C.E.J.L.L - NAVE RJ.

**Developers Team: Matheus Avellar, Nicole Freitas, Thiago Moraes and Victor Fernandes.**

Our project has the main reason to help Math's students with Matrix calculations.

It's developed in Visual Studio 2013 - Windows Presentation Foundation - C#.

##Features

The user can do calculations with matrix of size until 10 x 10. The operations are sum, subtraction, multiplication,
determinant, scalar multiplication, reverse and transpost matrix. Plus the graphic representation of translation, rotation and
scaling of 2D polygons using matrix calculation.

##Matrix Input

In other to use the calculations above you will need to know how to input your own matrix into the textBoxs.

The notation is:
```
1 2
3 4
```
Note: No blank space after the numbers.

### Sum 

This feature sum's two matrix of the same size. It simply sums every element in matrix1 with every element in matrix2.

### Subtraction

This feature subtracts two matrix of the same size. It simply subtracts every element in matrix1 with every element in matrix2.

### Multiplication

This feature multiplys two matrix which the first matrix collums number is equal to second matrix rows number. It multiplys each
row of the first by each collums of the second.

### Determinant

This feature founds the determinant of the first matrix. It uses the Chio method in combination with the determinant's properties.

### Scalar Multiplication 

This feature multiplys each element of the second matrix by the number at the first matrix's input textbox.

### Reverse

This feature founds the Reverse matrix of first matrix. It uses the adjugate matrix method.

[Adjugate Matrix Method](http://www.mathsisfun.com/algebra/matrix-inverse-minors-cofactors-adjugate.html)

### Transposed 

This feature founds the transposed matrix of the first matrix. It means it changes the rows by collums.

## String to Formula

This feature automatically creates an matrix of max size of 10 x 10 by a given formula. This formula can have basics operations.

```
"2 * i + j"
```
Note: The valid operations symbols are *,/,-,+

## Graphic

This feature allows you to create polygons in a 2D canvas. Simply click the canvas to add a point. In the top right hand corner
it will be added a two textboxes and a button written "Change". It means you can type the new x and y coordinates at the textboxes
than click "Change" and the graphic will be updated with the new coordinates.

#### Rotation 

This feature allows you to rotate the polygon you draw using canvas, using axis as the (0,0) point. Simply type the angle in degrees inside the textbox and click
"Rotate".

#### Translation

This feature allows you to increase the position of all points of the polygon by a given x and y coordinate.

#### Scaling

This feature allows you to scale the size of the polygon by a given x and y coordinate.

## Thank You For Using Matrix-Calculator!

My special thanks to my team, my teachers and my school!

Hope it helps you understand a little bit more about matrixs and all their applications.











