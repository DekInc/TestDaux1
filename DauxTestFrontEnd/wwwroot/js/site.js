$(function () {
  $('#formEjemplo').on('submit', function (e) {
    //var txtNombre = $('#txtNombre');
    //var txtApellido = $('#txtApellido');
    //console.log(txtNombre.validity);
    //if (txtNombre.validity.typeMismatch) {
    //  txtNombre.setCustomValidity('Please enter correct email');
    //} else {
    //  txtNombre.setCustomValidity('');
    //}
    e.preventDefault();
    var dataFormEjemplo = $("#formEjemplo").serializeArray();
    $.ajax({
      type: 'POST',
      data: dataFormEjemplo,
      datatype: 'json',
      success: function (dataResp) {
        console.log(1);
        $('#modalContenido').html(dataResp.respuesta);
        if (dataResp.mensajeError !== '')
          $('#modalContenido').addClass('divError');
        else
          $('#modalContenido').removeClass('divError');
        $('#modalResultado').modal('show');
      },
      error: function (jqXHR, textStatus, errorThrown) {
        $('#modalContenido').html(jqXHR.responseText + textStatus + errorThrown);
        $('#modalContenido').addClass('divError');
        $('#modalResultado').modal('show');
      }
    });
  });
  

  //submit.addEventListener('click', () => {
  //  if (email.validity.typeMismatch) {
  //    email.setCustomValidity('Please enter correct email');
  //  } else {
  //    email.setCustomValidity('');
  //  }
  //})
});