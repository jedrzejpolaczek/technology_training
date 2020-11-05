from flask import Flask
from flask import jsonify
from flask import request
from flask import render_template

app = Flask(__name__)
stores = [
    {
        'name': 'My Wonderful store',
        'items': [
            {
                'name': 'My item',
                'price': 15.99
            }
        ]
    }
]


@app.route('/')
def home():
    return render_template('index.html')

# POST - used to receive data
# GET - used to send data back only


# POST /store data: {name:}
@app.route('/store', methods=['POST'])
def create_store():
    request_data = request.get_json()
    new_store = {
        'name': request_data['name'],
        'items': []
    }
    stores.append(new_store)
    return jsonify(new_store)


# GET /store/<string:name>
@app.route('/store/<string:name>')  # Example: http://127.0.0.1:5000/store/some_name
def get_store(name):
    # Iterate over stores
    for store in stores:
        if store['name'] == name:
            # If the store name matches, return it
            return jsonify(store)
    # If none match, return an error message
    return jsonify({'message': 'Store not found.'})


# GET /store - list of all the stores
@app.route('/store')
def get_stores():
    return jsonify({'stores': stores})


# POST /store/<string:name>/item - create an item in specific store
@app.route('/store/<string:name>/item', methods=['POST'])
def create_item_in_store(name):
    request_data = request.get_json()
    # Iterate over stores
    for store in stores:
        if store['name'] == name:
            new_item = {
                'name': request_data['name'],
                'price': request_data['price']
            }
            store['items'].append(new_item)
            return jsonify(new_item)
    return jsonify({'message': 'Store not found.'})


# GET /store/<string:name>/item - get an item in specific store
@app.route('/store/<string:name>/item')
def get_item_in_store(name):
    # Iterate over stores
    for store in stores:
        if store['name'] == name:
            # If the store name matches, return it
            return jsonify({'items:': store['items']})
    # If none match, return an error message
    return jsonify({'message': 'Store not found.'})


app.run(port=5000)
