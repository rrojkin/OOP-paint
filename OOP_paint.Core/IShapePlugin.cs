namespace OOP_paint.ShapeModels
{
    // Контракт для плагина: даёт имя и умеет создавать ShapeBase.
    public interface IShapePlugin
    {
        // Отображаемое имя
        string Name { get; }

        /// Создаёт новый экземпляр фигуры.
        ShapeBase CreateShape();
    }
}
