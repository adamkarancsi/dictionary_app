import { valueToNode } from "@babel/types";
import React from "react";

interface IDictionarySearchProps {

}

interface IDictionarySearchState {
    value: string;
    result: string;
}

export default class DictionarySearch extends React.Component<IDictionarySearchProps, IDictionarySearchState> {

    constructor(props: IDictionarySearchProps) {
        super(props);

        this.state = {
            value: "",
            result: ""
        };
    }

    private onSearchComplete = (result: any) => {
        console.log(result);
    };

    private onSearchChange = (e: React.ChangeEvent<HTMLInputElement>) => {
        fetch("http://localhost:5025", {
            method: "GET",
            mode: "cors"
        }).then(this.onSearchComplete);

        this.setState(
            {
                ...this.state,
                value: e.target.value
            }
        );
    }

    public render() {

        const { value, result } = this.state;

        return (
            <div>
                <input onChange={this.onSearchChange} value={value}/>
                <span>{result}</span>
            </div>
        );
    }
}