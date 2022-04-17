var pathToDelete;

$(".deleteItem").click(function () {
	pathToDelete = $(this).data('path');
});

$("#btnContinueToDelete").click(function () {
	window.location = pathToDelete;
});