$(document).ready(function () {
    modalPlace.on('click','#btnChildAdd',e => {
        e.preventDefault();

        let baseId;
        let noteId;
        const isMain = $(clickedButton).prev().attr('data-bs-target');
        if (isMain.includes('main')) {
            baseId = isMain.substr(5);
            noteId = null;
        }
        else {
            baseId = $(clickedButton).prev().attr('id');
            noteId = isMain.substr(6);
        }
        $.ajax({
            url: '/Home/AddChildNote?baseId=' + baseId + '&noteId=' + noteId,
            type: 'POST',
            data: $("#childNoteForm").serialize(),
            success: function (response) {
                //en son eklenen not bilgisi
                const data = JSON.parse(response);
                const html = `
            <div class="accordion${data.id} ms-1">
                                    <div class="accordion-item">
                            <h2 class="accordion-header d-flex justify-content-around align-items-center" id="flush-headingOne">
                                <button class="accordion-button collapsed"
                                        type="button"
                                        data-bs-toggle="collapse"
                                        data-bs-target="#child${data.id}"
                                        aria-expanded="false"
                                        aria-controls="flush-collapseOne"
                                        id="${data.baseId}">
                                    ${data.title}
                                </button>
                                <button class="btn btn-outline-primary mx-1 btnChildAdd">+</button>
                                <button class="btn btn-outline-danger btnChildDelete">x</button>
                            </h2>
                            <div id="child${data.id}" class="accordion-collapse collapse"
                                 aria-labelledby="flush-headingOne"
                                 data-bs-parent="#child">
                                <div class="accordion-body">${data.content}</div>
                            </div>
                            </div>
                        </div>
            </div>
        `;
                noteId === null
                    ? $("#modalPlace").parent().find("#parent").find(`#main${baseId}`).append(html)
                    : $("#modalPlace").parent().find("#parent").find(`#child${noteId}`).append(html);
                $("#modalPlace").find('.modal').fadeOut();
                
            },
            error: function (err) {
                console.log(err)
            }
        })
    })
})