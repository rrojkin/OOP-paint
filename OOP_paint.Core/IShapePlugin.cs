using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP_paint.ShapeModels
{
    /// <summary>
    /// Контракт для плагина: даёт имя и умеет создавать ShapeBase.
    /// </summary>
    public interface IShapePlugin
    {
        /// <summary>Отображаемое имя (и имя кнопки).</summary>
        string Name { get; }

        /// <summary>Создаёт новый экземпляр фигуры.</summary>
        ShapeBase CreateShape();
    }
}
