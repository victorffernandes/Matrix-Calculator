# Matrix-Calculator

Matrix Calculator is an educational project brought to life by students of C.E.J.L.L - NAVE RJ.

**Developers Team:**

**[Matheus Avellar](https://github.com/MatheusAvellar), [Nicole Freitas](https://github.com/NicoleFreitas), [Thiago Moraes](https://github.com/othiago1) and [Victor Fernandes](https://github.com/zappknight).**

Our project's primary objective is to help Math students with Matrix calculations.

It is developed in Visual Studio 2013 - Windows Presentation Foundation - C#.

## Features

The user can do calculations with a matrix of size up to 10 x 10. The operations are sum, subtraction, multiplication,
determinant, scalar multiplication, inverse and transpost matrix. There is also the graphic representation of translation, rotation and scaling of 2D polygons using matrix calculation.
 
## Matrix Input

In order to successfully calculate the matrix operations showcased above, you will need to know how to input your own matrix into the text boxes.

The notation is the following:
```
1 2
3 4
```
Where columns are separated by spaces, and lines, by line breaks.
Note: There is no blank space after each line ending.

# Operations 

## Sum

This feature sums two matrixes of the same size. It simply sums every element in matrix #1 with every element in matrix #2.

```
1 2   |   1 2   |   2 4
3 4   |   3 4   |   6 8
```

## Subtraction

This feature subtracts two matrixes of the same size. It simply subtracts every element in matrix #1 with every element in matrix #2.

```
4 3   |   1 2   |    3  1
2 1   |   3 4   |   -1 -3
```

## Multiplication

This feature multiplies two matrixes if the first matrix's collums number is equal to second matrix's rows number. It multiplies every row of the first by every collum of the second.
```
1 2   |   1 2   |    7 10
3 4   |   3 4   |   15 22
```

## Determinant

This feature finds the determinant of the first matrix. It uses the Chio method in combination with the determinant's properties.
```
1 2   |   -2
3 4   |
```

## Scalar Multiplication 

This feature multiplies each element of the second matrix by the number at the first matrix's input textbox.
```
15   |   1 2   |   15 30
     |   3 4   |   45 60
```

## Inverse

This feature finds the inverse matrix of first matrix. It uses the [adjugate matrix method](http://www.mathsisfun.com/algebra/matrix-inverse-minors-cofactors-adjugate.html).

```
1 2   |    -2    1
3 4   |   1.5 -0.5
```

## Transposed

This feature finds the transposed matrix of the first matrix. It inverts rows by collums.

```
1 2   |   1 3
3 4   |   2 4
```

## String to Formula

This feature automatically creates a matrix of maximum size of 10 x 10 by a given formula. This formula can have basics operations.

```
2 * i + j
```
Note: The valid operations symbols are *, /, -, +

# Graphic

This feature allows you to create polygons in a 2D canvas. Simply click the canvas to add a point. In the top right hand corner, there will be two text boxes and a button labeled "Change". You may type the new x and y coordinates at the text boxes, and then click "Change". The graphic will be updated with the new coordinates.

## Rotation

This feature allows you to rotate the polygon you have drawn using the canvas, with axis at the (0, 0) point. Simply type the angle in degrees inside the text box and click "Rotate".

## Translation

This feature allows you to increase the position of all points of the polygon by given x and y values.

## Scaling

This feature allows you to scale the size of the polygon by given x and y values.

### Thank You For Using Matrix-Calculator!

My special thanks to my team, the teachers who requested this project, and my school!

Hopefully it helps you understand a little bit more about matrixes and all their applications.
