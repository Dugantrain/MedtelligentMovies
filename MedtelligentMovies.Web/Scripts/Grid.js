function PreserveClickedRowStyleOnMouseOut(gridViewId, hiddenFieldId) {
    var gridViewControl = document.getElementById(gridViewId);
    if (gridViewControl != null) {
        var gridViewRows = gridViewControl.rows;
        if (gridViewRows != null) {
            var selectedRowId = $("#" + hiddenFieldId).val();
            for (var i = 1; i < gridViewRows.length; i++) {
                if (selectedRowId == i) continue;
                var row = gridViewRows[i];
                row.style.backgroundColor = "";
            }
        }
    }
}

function ChangeMouseOverRowColor(gridViewId, rowId, hiddenFieldId) {
    var gridViewControl = document.getElementById(gridViewId);
    if (gridViewControl != null) {
        var gridViewRows = gridViewControl.rows;
        if (gridViewRows != null) {
            var selectedRowId = $("#" + hiddenFieldId).val();
            for (var i = 1; i < gridViewRows.length; i++) {
                var row = gridViewRows[i];
                if (i == selectedRowId) {
                    continue;
                } else if (i == rowId && rowId != selectedRowId) {
                    row.style.backgroundColor = "#f7fbfc";
                    $("#" + hiddenFieldId).val(selectedRowId);
                } else {
                    row.style.backgroundColor = "";
                }
            }
        }
    }
}

function ChangeSelectedRowColorOnClick(gridViewId, selectedRowId, hiddenFieldId) {
    var gridViewControl = document.getElementById(gridViewId);
    if (gridViewControl != null) {
        var gridViewRows = gridViewControl.rows;
        if (gridViewRows != null) {
            for (var i = 1; i < gridViewRows.length; i++) {
                var row = gridViewRows[i];
                if (selectedRowId == i) {
                    row.style.backgroundColor = "yellow";
                    $("#" + hiddenFieldId).val(selectedRowId);
                }
                else {
                    row.style.backgroundColor = "";
                }
            }
        }
    }
}