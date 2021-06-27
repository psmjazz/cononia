using System;
using System.Collections.Generic;
using System.Text;

namespace cononia.src.rx
{

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
