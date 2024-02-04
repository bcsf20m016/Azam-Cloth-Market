function initializeDataTables() {
    $("#product_table").DataTable();
    $("#category_table").DataTable();
}

window.onafterload = function () {
    initializeDataTables();
};

window.onunload = function () {
    // Optionally, destroy DataTables when leaving the page to prevent memory leaks
    $("#product_table").DataTable().destroy();
    $("#category_table").DataTable().destroy();
};
