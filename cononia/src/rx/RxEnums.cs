using System;
using System.Collections.Generic;
using System.Text;

namespace cononia.src.rx
{
    enum ERxNodeName
    {

        RxIngredientManager,
        RxIngredientController,

        // dynamic
        RxIngredientList,

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
