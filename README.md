# Clipping app (aka Клипующее приложение)

Приложение для демонстарции работы алгоритмов нахождения пересечений, построения выпуклых/невыпуклых оболочек и кластеризации.

Реализованные алгоритмы:

Пересечения многоугольников
* Convex polygon clipper код | [объяснение](./Core/Clippers/ConvexPolygonClipper/ConvexPolygonClipper.md)
* O`Rourke polygon clipper код | [объяснение](./Core/Clippers/RourkeChienPolygonClipper/RourkeChienPolygonClipper.md)
* Weiler-Atherton polygon clipper код | [объяснение](./Core/Clippers/WeilerAthertonPolygonClipper/WeilerAthertonPolygonClipper.md)

Кластеризация алгоритмом K-means с метриками:

* Евклидова
* Чебышева
* Косинусная

Построениe оболочек:

* QuickHull - выпуклые оболочки код | объяснение
* алгоритм Пугачёва - невыпуклые Оболочки код | объяснение

---

## Функционал приложения:

Окно для рисования полигонов:

![example1](./Imgs/PolygonDraw.png)

Нахождения пересечений полигонов как выпуклых, так и невыпукых

![example2](./Imgs/Intersection.png)

Для пересечения невыпуклых важно, чтобы вершины располагались по часовой стрелке

![example3](./Imgs/NonconvexIntersection.png)

Также в окне "Координаты" можно посмотреть координаты полученных многоугольников

![example2](./Imgs/InfoWindow.png)

Доступно окно для рисования точек для демонстрации алгоритмов кластеризации / построения оболочек

![example3](./Imgs/PointDrawWindow.png)

На полученный набор точек можно натянуть выпуклую оболочку или построить кластера

![example3](./Imgs/Convex.png)

![example3](./Imgs/ClusteringWindow.png)

На полученные кластера можно натянуть эллипсы / прямоугольники / выпуклые оболочки

![example3](./Imgs/EllipsesOnClusters.png)

![example3](./Imgs/EllipsesOnClusters2.png)

А после найти пересечения полученных фигур

![example3](./Imgs/EllipsesIntersection.png)
