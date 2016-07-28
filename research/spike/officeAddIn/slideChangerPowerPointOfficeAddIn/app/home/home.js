(function(){
  'use strict';

  // The initialize function must be run each time a new page is loaded
  Office.initialize = function(reason){
    jQuery(document).ready(function(){
      app.initialize();

      jQuery('#get-curr-slide-index').click(getCurrentSlideIndex);
      jQuery('#goto-next-slide').click(gotoNextSlide);
    });
  };

  // Gets the current slide index
  function getCurrentSlideIndex() {
    if (Office.context.document.getSelectedDataAsync) {
      Office.context.document.getSelectedDataAsync(Office.CoercionType.SlideRange, function(result) {
        if (result.status === Office.AsyncResultStatus.Succeeded) {
          app.showNotification('Current slide is:', '"' + result.value.slides[0] + '"');
        } else {
          app.showNotification('Error:', result.error.message);
        }
      });
    } else {
      app.showNotification('Error:', 'Slide index retreival is not supported!');
    }
  }

  // Moves to the next slide
  function gotoNextSlide() {
    var frst = Office.Index.First;
    var last = Office.Index.Last;
    var next = Office.Index.Next;
    var prev = Office.Index.Previous;

    Office.context.document.goToByIdAsync(Next, Office.GoToType.Index, function(result) {
        if (result.status === Office.AsyncResultStatus.Succeeded) {
            app.showNotification("Next slide successful");
        } else {
            app.showNotification("Error: " + result.error.message);
        }
    });
  }
})();
