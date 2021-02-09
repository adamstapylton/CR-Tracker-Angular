function closeModal(modalId) {
  $(modalId).modal('toggle');
}

$(document).delegate('ul', 'mouseover', function () {
  $('.sortableList').sortable({
    connectWith: '.connectedList',
    stop: function (event, ui) {
      let stageId = ui.item.closest('.cr-kanban-list').attr('data-id');
      let crId = ui.item.attr('data-id');
      updateCrStage(crId, stageId, ui.item);
    }
  });
})

function updateCrStage(crId, stageId, item) {
  $.ajax({
    type: 'PATCH',
    url: `api/changeRequests/${crId}?stageId=${stageId}`,
    context: 'application/json',
    dataType: 'json',
    success: function (status) {
      item.addClass('item-moved');
    },
    error: function (data) {
      console.log('Error: ', data);
      return false;
    }
  })
};
