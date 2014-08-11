/*
* 用户管理DataGrid服务端查询接口
*/
function queryParams(params) {
    return {
        Limit: params.pageSize,
        Offset: params.pageSize * (params.pageNumber - 1),
        Search: params.searchText,
        SortName: params.sortName,
        Order: params.sortOrder
    };
}

function operateFormatter(value, row, index) {
    return ['<div>',
        '<a href="javascript:void(0)" class="btn btn-small edit">编辑<i class="icon-edit"></i></a>&nbsp;',
        '<a href="javascript:void(0)" class="btn btn-small btn-danger remove">删除<i class="icon-remove"></i></a>',
        '</div>'].join('');
}

function dateFormatter(value, row, index) {
    var date = new Date(parseInt(value.substr(6)));
    return DateFormat.format.date(date, 'yyyy-MM-dd hh:mm:ss');
}

function statusFormater(value, row, index) {
    return value == 0 ? "有效" : "已阻止";
}

window.operateEvents = {
    'click .edit': function (e, value, row, index) {
        var title = '编辑用户:' + row['RealName'];
        jQuery('#modalTitle').html(title);
        jQuery('#modalBox').modal({ backdrop: 'static', keyboard: false, remote: '/SysAdmin/UserEdit/' + row['UserName'] });
    },
    'click .remove': function (e, value, row, index) {
        alert('You click remove icon, row: ' + JSON.stringify(row));
    }
};

jQuery('#table-users').bootstrapTable().on('load-error.bs.table', function (e, res) {
    var jsonRes;
    eval('jsonRes = ' + res.responseText);
    jQuery('#msgUser').html(res.status + ':' + jsonRes.Message);
    jQuery('#alertDiv').show();
});