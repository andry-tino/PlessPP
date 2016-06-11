/// <reference path="../App.js" />
// global app

(function () {
    'use strict';

    // The initialize function must be run each time a new page is loaded
    Office.initialize = function (reason) {
        $(document).ready(function () {
            app.initialize();

            $('#nextSlide').click(nextSlide);
        });
    };

    function gotoSlide() {
        var first = Office.Index.First;
        var last = Office.Index.Last;
        var next = Office.Index.Next;
        var prev = Office.Index.Previous;

        Office.context.document.goToByIdAsync(Next, Office.GoToType.Index, function(data) {
            if (data.status == "failed") {
                app.showNotification("Error: " + data.error.message);
            } else {
                app.showNotification("Next slide successful");
            }
        });
    }
})();