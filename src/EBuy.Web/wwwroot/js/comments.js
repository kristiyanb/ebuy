const baseUrl = 'http://localhost:50329/api/comments/';

async function loadComments() {
    let commentsSection = document.getElementById("comments");
    let comments = await getComments();

    let html = comments.map(x => {
        return `<div class="d-flex justify-content-between">
                        <div>
                            <h6>${x.content}</h6>
                        </div>
                        <div class="mr-3">
                            ${x.username}
                            <br>
                            ${x.lastModified}
                            <br>
                        </div>
                    </div>
                    <hr />`;
    });

    commentsSection.innerHTML = html.join('');
}

async function addComment() {
    let content = document.getElementById('content').value;
    let productId = document.getElementById('productId').value;
    let username = document.getElementById('username').value;

    let url = baseUrl + 'create';

    let comment = {
        content,
        productId,
        username
    };

    await fetch(url, {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json',
        },
        body: JSON.stringify(comment)
    });

    await loadComments();
    document.getElementById('content').value = '';
}

async function getComments() {
    let id = window.location.href.split('/')[5];
    let url = baseUrl + id;

    let response = await fetch(url);

    return response.json();
}

loadComments();

document.getElementById('add-comment').addEventListener('click', addComment);