from flask import Flask, jsonify, request
import pandas as pd
from pandas import ExcelFile
import os
app = Flask(__name__)


@app.route("/get_attr_name/<id>", methods=["GET", "POST"])
def map_attribute_values(id):
    _id = request.view_args['id']
    df = pd.read_excel('nutritionix.xlsx')
    return df.loc[df['attr_id'] == int(_id)].name.to_csv()


if __name__ == '__main__':
    app.run(debug=True)
