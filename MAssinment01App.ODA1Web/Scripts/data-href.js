/// <reference path="jquery-1.10.2.js" />
$(function () {
	//console.log("data-href");
	$("[data-href]").bind("click", function () {
		document.location = $(this).attr("data-href");
	});
});