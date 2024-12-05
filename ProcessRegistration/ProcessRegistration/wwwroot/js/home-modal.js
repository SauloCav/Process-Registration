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
            })
            .catch(error => {
                modalContent.innerHTML = `<p class="text-danger">Erro ao carregar: ${error.message}</p>`;
            });
    });
});
