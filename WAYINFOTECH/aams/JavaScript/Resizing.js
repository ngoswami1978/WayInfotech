var sResizableElement = "TH";    // This MUST be upper case
var iResizeThreshold = 8;
var iEdgeThreshold = 8;
var iSizeThreshold = 20;
var sVBarID = "VBar";

var oResizeTarget = null;

var iStartX = null;

var iEndX = null;

var iSizeX = null;

    function TableResize_CreateVBar()

{

    // Returns a reference to the resizer VBar for the table

    var objItem = document.getElementById(sVBarID);

    // Check if the item doesn't yet exist

    if (!objItem) 

    {        

        // and Create the item if necessary

        objItem = document.createElement("SPAN");

        // Setup the bar

        objItem.id = sVBarID;

        objItem.style.position = "absolute";

        objItem.style.top = "0px";

        objItem.style.left = "0px";

        objItem.style.height = "0px";

        objItem.style.width = "2px";

        objItem.style.background = "silver";

        objItem.style.borderLeft = "1px solid black";

        objItem.style.display = "none"; 
        document.body.appendChild(objItem);

    }

}

window.attachEvent("onload", TableResize_CreateVBar);

function TableResize_GetOwnerHeader(objReference) 

{

    var oElement = objReference;

    while (oElement != null && oElement.tagName != null && oElement.tagName != "BODY") 

    {

        if (oElement.tagName.toUpperCase() == sResizableElement) 

        {

            return oElement;

        }
        oElement = oElement.parentElement;

    }

    // The TH wasn't found

    return null;

}

function TableResize_GetFirstColumnCell(objTable, iCellIndex) 
{

    var oHeaderCell = objTable.rows(0).cells(iCellIndex);
    return oHeaderCell;
}

function TableResize_CleanUp() 
{
    var oVBar = document.getElementById(sVBarID);
    if (oVBar)
    {
        oVBar.runtimeStyle.display = "none";
    }
    iEndX = null;

    iSizeX = null;

    iStartX = null;

    oResizeTarget = null;

    oAdjacentCell = null;
    return true;

}

function TableResize_OnMouseMove(objTable) 
{
    // Change cursor and store cursor position for resize indicator on column

    var objTH = TableResize_GetOwnerHeader(event.srcElement);

    if (!objTH)

        return;

    var oVBar = document.getElementById(sVBarID);

    if (!oVBar)

        return;

    var oAdjacentCell = objTH.nextSibling;

    if ((event.offsetX >= (objTH.offsetWidth - iEdgeThreshold)) && (oAdjacentCell != null)) 

    {

        objTH.runtimeStyle.cursor = "e-resize";

    } 

    else 

    {

        if(objTH.style.cursor) 

        {

            objTH.runtimeStyle.cursor = objTH.style.cursor;

        } 

        else 

        {

            objTH.runtimeStyle.cursor = "";

        }

    }

    if (oVBar.runtimeStyle.display == "inline") 

    {

        oVBar.runtimeStyle.left = window.event.clientX + document.body.scrollLeft;
        document.selection.empty();

    }

    return true;
}

function TableResize_OnMouseDown(objTable) 
{
    var oTargetCell = event.srcElement;
    if (!oTargetCell)

        return;

    var oVBar = document.getElementById(sVBarID);

    if (!oVBar)

        return;


    if (oTargetCell.parentElement.tagName.toUpperCase() == sResizableElement)

    {
        oTargetCell = oTargetCell.parentElement;
    }

    var oHeaderCell = TableResize_GetFirstColumnCell(objTable, oTargetCell.cellIndex);



    if ((oHeaderCell.tagName.toUpperCase() == sResizableElement) && (oTargetCell.runtimeStyle.cursor == "e-resize")) 

    {        
        iStartX = event.screenX;

        oResizeTarget = oHeaderCell;

        objTable.setAttribute("Resizing", "true");

        objTable.setCapture();
        oVBar.runtimeStyle.left = window.event.clientX + document.body.scrollLeft;

        oVBar.runtimeStyle.top = objTable.parentElement.offsetTop + objTable.offsetTop;;

        oVBar.runtimeStyle.height = objTable.parentElement.clientHeight;

        oVBar.runtimeStyle.display = "inline";

    }

    return true;
}

function TableResize_OnMouseUp(objTable) 
{
    // Resize the column and its adjacent sibling if position and size are within threshold values

    var oAdjacentCell = null;

    var iAdjCellOldWidth = 0;

    var iResizeOldWidth = 0;

    if (iStartX != null && oResizeTarget != null) 

    {

        iEndX = event.screenX;

        iSizeX = iEndX - iStartX;

        objTable.setAttribute("Resizing", "false");



        if ((oResizeTarget.offsetWidth + iSizeX) >= iSizeThreshold) 

        {

            if (Math.abs(iSizeX) >= iResizeThreshold) 

            {

                if (oResizeTarget.nextSibling != null) 

                {
                    oAdjacentCell = oResizeTarget.nextSibling;
                    iAdjCellOldWidth = (oAdjacentCell.offsetWidth);
                } 

                else 
                {
                    oAdjacentCell = null;
                }

                iResizeOldWidth = (oResizeTarget.offsetWidth);

                oResizeTarget.style.width = iResizeOldWidth + iSizeX;

                if ((oAdjacentCell != null) && (oAdjacentCell.tagName.toUpperCase() == sResizableElement)) 
                {
                    oAdjacentCell.style.width = (((iAdjCellOldWidth - iSizeX) >= iSizeThreshold)?(iAdjCellOldWidth - iSizeX):(oAdjacentCell.style.width = iSizeThreshold))
                }

           }
        } 
        else 
        {
            oResizeTarget.style.width = iSizeThreshold;
        }

    }

    TableResize_CleanUp();

    objTable.releaseCapture();

    return true;

}


