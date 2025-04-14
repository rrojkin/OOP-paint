using OOP_paint.ShapeModels;

namespace OOP_paint
{
    public class UndoRedoManager
    {
        private Stack<ShapeBase> undoStack = new();
        private Stack<ShapeBase> redoStack = new();
        private ShapeBase currentShape = null;

        public void Undo(List<ShapeBase> shapes)
        {
            if (undoStack.Count > 0)
            {
                var shape = undoStack.Pop();
                shapes.Remove(shape);
                redoStack.Push(shape);
            }
        }
        public void Redo(List<ShapeBase> shapes)
        {
            if (redoStack.Count > 0)
            {
                var shape = redoStack.Pop();
                shapes.Add(shape);
                undoStack.Push(shape);
            }
        }
        public void Push(ShapeBase shape)
        {
            if (shape != null)
            {
                undoStack.Push(shape);
                redoStack.Clear();
            }
        }
    }
}
