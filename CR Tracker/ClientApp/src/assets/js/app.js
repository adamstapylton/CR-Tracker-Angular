function closeModal(modalId) {
  $(modalId).modal('toggle');
}

$(document).delegate('li', 'mousedown', function () {
  $('.sortable').sortable();
})
