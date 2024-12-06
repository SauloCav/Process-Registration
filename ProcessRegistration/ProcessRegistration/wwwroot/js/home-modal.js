document.addEventListener('DOMContentLoaded', function () {
    const modal = document.getElementById('actionModal');
    const modalTitle = document.getElementById('actionModalLabel');
    const modalContent = document.getElementById('modalContent');

    modal.addEventListener('show.bs.modal', function (event) {
        const button = event.relatedTarget;
        const title = button.getAttribute('data-title');
        const actionUrl = button.getAttribute('data-action');

        modalTitle.textContent = title;
        modalContent.innerHTML = '<p class="text-center">Carregando...</p>';

        fetch(actionUrl)
            .then(response => {
                if (!response.ok) throw new Error('Erro ao carregar conteúdo');
                return response.text();
            })
            .then(html => {
                modalContent.innerHTML = html;
                attachEventListeners();
                validateForm();
            })
            .catch(error => {
                modalContent.innerHTML = `<p class="text-danger">Erro ao carregar: ${error.message}</p>`;
            });
    });
});

function attachEventListeners() {
    $(document).off('change', '#State').on('change', '#State', function () {
        var state = $(this).val();
        if (state) {
            $.getJSON('https://servicodados.ibge.gov.br/api/v1/localidades/estados/' + state + '/municipios')
                .done(function (data) {
                    var cityOptions = '<option value="">Selecione o Município</option>';
                    $.each(data, function (key, city) {
                        cityOptions += '<option value="' + city.id + '">' + city.nome + '</option>';
                    });
                    $('#City').html(cityOptions);
                })
                .fail(function () {
                    alert('Erro ao carregar os municípios. Por favor, tente novamente.');
                });
        } else {
            $('#City').html('<option value="">Selecione o Município</option>');
        }
    });

    $(document).off('click', '#btnSave').on('click', '#btnSave', function () {
        $('#loadingOverlay').show();
        $('#btnSave').prop('disabled', true);

        var name = $('#Name').val();
        var npu = $('#NPU').val();
        var state = $('#State').val();
        var cityId = $('#City').val();

        var formData = {
            Name: name,
            NPU: npu,
            State: state,
            City: cityId
        };

        $.ajax({
            url: '/Process/Create',
            type: 'POST',
            contentType: 'application/json',
            data: JSON.stringify(formData),
            headers: {
                'Accept': 'application/json',
                'Content-Type': 'application/json'
            },
            success: function (response) {
                $('#loadingOverlay').hide();
                alert(response.message);
                $('#formProcess')[0].reset();
                $('#actionModal').modal('hide');
                location.reload();
            },
            error: function (xhr) {
                $('#loadingOverlay').hide();
                alert('Ocorreu um erro ao cadastrar o processo: ' + xhr.responseText);
            },
            complete: function () {
                $('#btnSave').prop('disabled', false);
            }
        });
    });

    $(document).off('click', '#btnSaveEdit').on('click', '#btnSaveEdit', function () {
        $('#loadingOverlay').show();
        $('#btnSaveEdit').prop('disabled', true);

        var id = $('#Id').val();
        var name = $('#Name').val();
        var npu = $('#NPU').val();
        var state = $('#State').val();
        var city = $('#City').val();

        var formData = {
            Id: id,
            Name: name,
            NPU: npu,
            State: state,
            City: city
        };

        $.ajax({
            url: '/Process/Edit',
            type: 'POST',
            contentType: 'application/json',
            data: JSON.stringify(formData),
            headers: {
                'Accept': 'application/json',
                'Content-Type': 'application/json'
            },
            success: function (response) {
                $('#loadingOverlay').hide();
                alert('Processo atualizado com sucesso!');
                $('#actionModal').modal('hide');
                location.reload();
            },
            error: function (xhr) {
                $('#loadingOverlay').hide();
                alert('Ocorreu um erro ao atualizar o processo: ' + xhr.responseText);
            },
            complete: function () {
                $('#btnSaveEdit').prop('disabled', false);
            }
        });
    });


    $(document).off('click', '#confirmDeleteButton').on('click', '#confirmDeleteButton', function () {
        const processId = $('#processId').val();
        $('#loadingOverlay').show();

        $.ajax({
            url: '/Process/DeleteConfirmed/' + processId,
            type: 'POST',
            success: function (response) {
                $('#loadingOverlay').hide();
                if (response.success) {
                    alert('Processo excluído com sucesso!');
                    $('#actionModal').modal('hide');
                    location.reload();
                } else {
                    alert('Erro ao excluir o processo: ' + response.message);
                }
            },
            error: function (xhr, status, error) {
                $('#loadingOverlay').hide();
                console.error("Status: " + status);
                console.error("Erro: " + error);
                alert('Ocorreu um erro ao excluir o processo: ' + xhr.responseText);
            }
        });
    });


    $(document).on('input change', '#formProcess input, #formProcess select', function () {
        validateForm();
    });
}

function validateForm() {
    let isFormValid = true;
    $('#formProcess').find('input, select').each(function () {
        if ($(this).prop('required') && $(this).val() === "") {
            isFormValid = false;
            return false;
        }
    });

    $('#btnSave').prop('disabled', !isFormValid);
}

$(document).ready(function () {
    attachEventListeners();
    validateForm();
});
