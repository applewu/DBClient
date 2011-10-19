/* 
$Id: jquery.jgrideditor.js,v 1.3 2009/09/24 marco@zoqui.com $ 
*/

/**
* jQuery Grid Editor JQuery Plugin 
*
* @name  jgrideditor
* @type  jQuery
* @param String  postURL 	   POST METHOD URL used to send edited cell content after enter be pressed
* @param String  getURL	   GET METHOD URL to retrieve asyncronous table data. Used only if no rows is defined
* @param String  getMsg 	   String Default message that will be displayed when loading row data
* @param String  postMsg 	   String Default message that will be displayed when posting cell data
* @param String  classname    a css class name for grid formating
* @param String  header       String that will be displayed above the header
* @param Array   cols         Array of json objects which describes the column and their types
* @param Array   rows         Array of json data that feeds the table. If set getURL is not used 
*                             it asynconrous 
*                             Dictionary keys are column names attributes
* -------------------------------------------
*    Data definition sample: 
*      cols : [  
*                {id: 'hour', label: 'Hour', editable: false, type: '',  width: 'auto'},
*                {id: 'name', label: 'Patient Name', editable: true, type: 'input',  width: 'auto'},
*                {id: 'phone', label: 'Patient Phone', editable: true, type: 'input', width: 'auto'} 
*              ]
*
*      Notice that "hour", "name" and "phone" must have correspondent values for cols.id
*      rowid will be send back when posting.
*      rows : [  
*                {rowid: 'xyzabc', hour: '13:30', name: 'Machado de assis', phone : 'not applicable'},
*                {rowid: 'xyzabc', hour: '14:30', name: 'Sylvia Plath', phone : '71 9451-7845'},
*                {rowid: 'xyzabc', hour: '15:30', name: 'Carl Sagan', phone : '81 8794-7845'}
*             ]
*
*
**/
/**
* $Id: Json2Table v 1.0 2009/04/18 marco@zoqui.com $ 
* Class Json2Table Translate a JSData to a table
**/
function JSON2Table() {
    // Attributes
    this.classname = '';
    this.bufCols = new Array();
    this.bufRows = new Array();

    this.rows = [];
    this.cols = [];

    // Methods
    this.setCols = function(cols) {
        this.cols = cols;
        this.bufCols.push("<tr>");
        for (i = 0; i < cols.length; i++) {
            var width = cols[i].width;
            this.bufCols.push('<th style="width:' + cols[i].width + ';">' + cols[i].label + '</th>');
        };
        this.bufCols.push("</tr>");
    };

    this.setRows = function(rows) {
        this.rows = rows;
        for (iRow = 0; iRow < rows.length; iRow++) {
            var rowid = 'Row' + iRow;
            if (rows[iRow]['rowid'])
                rowid = rows[iRow]['rowid'];
            this.bufRows.push('<tr id="' + rowid + '" class="editabletablerow">');
            for (iCol = 0; iCol < this.cols.length; iCol++) {
                this.bufRows.push('<td>' + rows[iRow][this.cols[iCol].id] + '</td>');
            };
            this.bufRows.push("</tr>");
        };
    };

    this.getAsHtml = function() {
        var cls = "";
        if (self.classname !== "")
            cls = "class='" + this.classname + "'";
        var ret = "<table " + cls + ">" + this.bufCols.join('') + this.bufRows.join('') + "</table>";
        //alert(ret);
        return ret;
    };
};

var KeyCodeMap = {
    pagedown: 34,
    right: 39,
    down: 40,
    pageup: 33,
    left: 37,
    up: 38,
    esc: 27,
    enter: 13,
    tab: 9,
    f2: 113,
    f4: 115,
    del: 46
};

/**
* $Id: GridEditor v 1.0 2009/04/18 marco@zoqui.com $ 
* Class GridEditor Handle document events for html table editing
**/
function GridEditor(id, settings) {
    // Properties
    this.id = id; // divId
    this.settings = settings;
    this.editing = false;
    this.selRow = 1;
    this.selCol = 0;
    this.cells;
    this.nRows = 0;
    this.nCols = 0;
    this.lastchar = '';
    // Returns true if the table is valid
    this.isReady = function() {
        return ((this.nRows !== 0) && (this.nCols !== 0));
    };

    // Initialize the object
    this.init = function() {
        this.cells = jQuery("#" + this.id + " > table td");
        this.nRows = jQuery("#" + this.id + " > table tr").length;
        this.nCols = Math.round(this.nRows > 0 ? jQuery("#" + this.id + "> table td,th").length / this.nRows : 0);
        if (this.isReady()) {
            this.activateCell(this.selRow, this.selCol);
            this.listenKeyboard();
            return true;
        }
        return false;
    };

    // Set the current selection
    this.activateCell = function(r, c) {
        if ((this.nRows === 0) || (this.nCols === 0))
            return; // Not Initialized
        if (r > this.nRows - 1)
            r = 1;
        else if (r < 1)
            r = this.nRows - 1;
        if (c > this.nCols - 1)
            c = 0;
        else if (c < 0)
            c = this.nCols - 1;
        jQuery(".selectedcell").removeClass();
        var sel = "#" + this.id + " > table tr:eq(" + r + ") td:eq(" + c + ")";
        var activeCell = jQuery(sel);
        activeCell.addClass("selectedcell");
        this.selRow = r;
        this.selCol = c;
        return activeCell;
    };

    // Process the keydown event on the document
    this.listenKeyboard = function() {
        var self = this; // keep context
        if (!self.isReady())
            return;
        jQuery(document).keydown(function(e) {
            if (self.editing)
                return;
            self.lastchar = '';
            switch (e.keyCode) {
                case KeyCodeMap.esc:
                    e.preventDefault();
                    break;
                case KeyCodeMap.enter:
                case KeyCodeMap.f2:
                    e.preventDefault();
                    jQuery(".selectedcell").click();
                    break;
                case KeyCodeMap.left:
                    self.activateCell(self.selRow, self.selCol - 1);
                    e.preventDefault();
                    break;
                case KeyCodeMap.tab:
                case KeyCodeMap.right:
                    self.activateCell(self.selRow, self.selCol + 1);
                    e.preventDefault();
                    break;
                case KeyCodeMap.down:
                    self.activateCell(self.selRow + 1, self.selCol);
                    e.preventDefault();
                    break;
                case KeyCodeMap.up:
                    self.activateCell(self.selRow - 1, self.selCol);
                    e.preventDefault();
                    break;
                case KeyCodeMap.del:
                    e.preventDefault();
                    self.lastchar = 'DEL';
                    jQuery(".selectedcell").click();
                    break;
                default:
                    if ((e.keyCode >= 65 && e.keyCode <= 90) || (e.keyCode >= 48 && e.keyCode <= 57) || (e.keyCode >= 97 && e.keyCode <= 122)) {
                        e.preventDefault();
                        self.lastchar = String.fromCharCode(e.keyCode);
                        jQuery(".selectedcell").click();
                    }
            }
        });
    };

    this.enable = function() {
        var self = this; // keep context
        if (!self.isReady())
            return;
        // loop on cells
        jQuery(this.cells).each(function(i) {
            jQuery(this)['click'](function() {
                jQuery("#messages").hide();
                if (self.editing) return;
                var row = parseInt(i / self.nCols);
                var col = parseInt(i % self.nCols);
                self.activateCell(row + 1, col);
                if (!self.settings.cols[col].editable) return;
                self.editing = true;
                var cell = this;
                var rowid = jQuery(cell).parent().get(0).id
                var width = jQuery(cell).css('width');
                cell.previousHtml = jQuery(this).html();
                cell.previousData = jQuery(this).text();
                cell.innerHTML = '';
                navEnable = false;
                /* create the form object */
                var celForm = document.createElement('form');
                var celInput = document.createElement('input');
                celForm.setAttribute("id", 'editcelform');
                celInput.setAttribute('type', 'input');
                jQuery(celInput).addClass('celinput');
                jQuery(celInput).css('width', width);
                celInput.setAttribute('autocomplete', 'off');
                var deleting = false;
                if (self.lastchar == '') {
                    celInput.value = cell.previousData;
                }
                else {
                    if (self.lastchar == "DEL") {
                        celInput.value = "";
                        deleting = true;
                    }
                    else {
                        celInput.value = self.lastchar;
                    }
                }
                self.lastchar = "";
                celInput.name = self.settings.cols[col].id;
                celForm.appendChild(celInput);
                // Append the form to the TD Table CELL and set focus
                cell.appendChild(celForm);
                if (deleting) {
                    ajaxPost();
                }
                else {
                    celInput.focus();
                    jQuery(celInput).keydown(function(e) {
                        if (e.keyCode == KeyCodeMap.esc) {
                            e.preventDefault();
                            reset();
                        }
                        else if (e.keyCode == KeyCodeMap.enter) {
                            jQuery(celForm).submit();
                        }
                    });
                    var timer;
                    jQuery(celInput).blur(function(e) {
                        timer = setTimeout(reset, 10);
                    });
                }
                jQuery(celForm).submit(function(e) {
                    if (timer) {
                        clearTimeout(timer);
                    }
                    e.preventDefault();
                    ajaxPost();
                });
                function ajaxPost() {
                    // Ajax Posting...
                    if (self.settings.postMsg)
                        jQuery(cell).html(self.settings.postMsg);
                    var cellData = jQuery(celInput).val();
                    var postData = {};
                    postData['fieldname'] = celInput.name;
                    postData['fieldvalue'] = cellData;
                    postData['row'] = row;
                    postData['col'] = col;
                    postData['rowid'] = rowid;
                    if (self.settings.postURL === '') {
                        jQuery(cell).text(cellData);
                    }
                    else {
                        jQuery.post(self.settings.postURL, postData, function(str) {
                            var resp = eval('(' + str + ')');
                            if (resp.sts == "ERROR") {
                                var msg = '<span style="color:#FF0000;z-index:2;text-align:center;">' + resp.msg + '</span>';
                                warn(msg);
                                setTimeout(reset, 1000);
                            }
                            else {
                                jQuery(cell).html(resp.fieldvalue);
                            }
                            self.editing = false;
                        });
                    };
                    return;
                };
                function reset() {
                    jQuery(cell).html(cell.previousHtml);
                    self.editing = false;
                };
                function warn(message) {
                    jQuery("#messages").html(message).show().click(function() {
                        jQuery(this).hide();
                    })
                };
            });
        });
    };
};

// JQuery Plugin
jQuery.fn.jgrideditor = function(options) {
    var settings = {
        postURL: '',
        getURL: '',
        getMsg: 'Loading...',
        postMsg: 'Saving...',
        tableClass: 'editabletable',
        cellClass: 'selectedcell',
        header: '',
        cols: [],
        rows: []
    };
    if (options) {
        jQuery.extend(settings, options);
    };
    // My Id
    var myId = jQuery(this).attr("id");
    // Are there any cols?
    if (settings.cols.length === 0) {
        jQuery(this).append('Field array not defined');
        return jQuery(this);
    };
    // Default Editable Grid Style
    if (settings.tableClass === "editabletable")
        jQuery("head").append("<style>\
.editabletable{ color: #000;border-collapse: collapse;margin: 2px; background: #FFFFFF; border-top: 1px solid #000;border-left: 1px solid #000;empty-cells:show;}\
.editabletable th {background: #efd;padding-left: 2px;padding-right: 2px;color: #000;border-right: 1px solid #000;border-bottom: 1px solid #000;}\
.editabletable tr {height: 20px;}\
.editabletable td {padding-left: 2px;padding-right: 2px;border-right: 1px solid #000;border-bottom: 1px solid #000;	color: #000;font-size: 11px; font-family: tahoma,verdana,sans-serif;}\
.editabletable .even {background-color: #3D3D3D;}\
.editabletable .odd {background-color: #6E6E6E;}\
.editabletable .header {background-repeat: no-repeat;padding-left: 4px;padding-right: 1px;height: 20px;text-align:center;}\
.celinput {height: 13px; background-color: transparent; font-size: 11px; font-family:  tahoma,verdana,sans-serif; border:1px; padding-top: 1px; margin-left: -1px;}\
.clickbox{position:relative;top:12px;	margin-right:-2px;margin-top:-14px;float:right;width:8px;height:8px;border:1px solid #565;background:#FAAA00;}\
</style>");
    if (settings.cellClass === "selectedcell")
        jQuery("head").append("<style>\
.selectedcell{background-color: #D6D7E1;border-style: dotted;border-width: 1px;padding: 1px 1px 1px 1px;font-size: 11px; font-family: tahoma,verdana,sans-serif;}\
</style>");
    function edit() {
        var editor = new GridEditor(myId, settings);
        if (editor.init()) {
            editor.enable();
        };
    }
    // Creates the table inside the given node (div)
    if (settings.rows.length > 0) {
        var tbl = new JSON2Table();
        tbl.classname = settings.tableClass;
        tbl.setCols(settings.cols);
        tbl.setRows(settings.rows);
        jQuery("#" + myId).append(tbl.getAsHtml());
        edit();
    }
    else {
        if (settings.getURL === '') {
            jQuery("#" + myId).append('<div id="messages">datasource not defined</div>').find("div").fadeOut(3000);
        }
        else {
            jQuery("#" + myId).append('<div id="messages" style="position: absolute; width:400px;border: 2px solid #E50000; background: #FFFFFF">' + settings.getMsg + '</div>').find("div").fadeOut(5000);
            jQuery.get(settings.getURL, function(jsonObject) {
                jQuery("#" + myId).find("div").hide()
                var tbl = new JSON2Table();
                tbl.classname = settings.tableClass;
                tbl.setCols(settings.cols);
                tbl.setRows(eval('(' + jsonObject + ')'));
                jQuery("#" + myId).append(tbl.getAsHtml());
                jQuery(".editabletablerow").hover(
                function() {
                    var frm = jQuery("#editcelform").is(':visible');
                    if (!frm)
                        $(this).css("backgroundColor", "#def");
                },
                function() {
                    $(this).css("backgroundColor", "#fff");
                }
                );
                edit();
            });
        }
    }
    return (this);
}
