# ConvexPolygonClipper O(N^2^)

## ��� �������� �������� ���������� ����������� �������� ���������

������ �������� �������� ������ � �������� �����������.

� ��������� ������������ ������� ������� ��������:
1. ���������� ����������� ���� �������� O(1).
1. ����������� ���������� ����� � �������� O(N).
1. ���������� ����� �� ������� ������� �(N*log(N)).

����� ���������:
1. ���������� ����� ������� ��������, ������� ��������� � �������� 2 �(N^2^).
1. ���������� ����� ������� ��������, ������� ��������� � �������� 1 O(N^2^).
1. ���������� ����� ����������� ���� ��������� �(N^2^):
    1. ���� ������ ����� ������� �������� - ��������� ���. 
    1. ���������� ������ ����� ������� �������� � �������� ����� ����������� � ������������� ������.
1. ��������� ��� ��������� ����� �� ������� ������� �(N*log(N)).

![example1](./Imgs/exm1.png)
![example2](./Imgs/exm2.png)
![example3](./Imgs/exm3.png)