using System;
using System.Collections.Generic;
using System.Text;

namespace cononia.src.rx
{
    public enum EUpdateEvent
    {
        UpdateIngredientList
    }

    enum ERxEventType
    {
        EventCreateNode,
        EventDeleteNode
    }

    enum ECommand
    {
        CommandLoadIngredients,
        CommandGetIngredients,
        CommandUpdateIngredient,
        CommandInsertIngredient,
        CommandDeleteIngredient
    }
}
