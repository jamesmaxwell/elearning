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