$(document).ready(function () {
    $("#modalPlace").on('click','#btnMainAdd',e => {
        e.preventDefault();
        $.ajax({
            url: '/Home/AddMainNote',
            type: 'POST',
            data: $("#mainNoteForm").serialize(),
            success: function (response) {
                //en son eklenen not bilgisi
                const data = JSON.parse(response);
                const html = `
                        <div class="accordion-item">
                            <h2 class="accordion-header d-flex justify-content-around align-items-center" id="flush-headingOne">
                                <button class="accordion-button collapsed"
                                        type="button"
                                        data-bs-toggle="collapse"
                                        data-bs-target="#main${data.id}"
                                        aria-expanded="false"
                                        aria-controls="flush-collapseOne">
                                    ${data.title}
                                </button>
                                <button class="btn btn-outline-primary mx-1 btnChildAdd">+</button>
                                <button class="btn btn-outline-danger btnMainDelete">x</button>
                            </h2>
                            <div id="main${data.id}" class="accordion-collapse collapse"
                                 aria-labelledby="flush-headingOne"
                                 data-bs-parent="#parent">
                                <div id="mainContent${data.id}" class="accordion-body">${data.content}</div>
                            </div>
                            </div>
                        </div>
                `;
                $("#modalPlace").parent().find("#parent").append(html);
                $("#modalPlace").find('.modal').fadeOut();
            },
            error: function (err) {
                console.log(err)
            }
        })
    })
})