package main // import "go-server2"

import (
	"bufio"
	"fmt"
	"os"

	"namedpipe"
)

// Message - 메시지
type Message struct {
	Message string
}

func readData(reqID string, pipe namedpipe.NamedPipeServer) error {
	reader, err := pipe.GetReader(reqID)
	if err != nil {
		fmt.Printf("Unable to get reader: %+v", err)
		return err
	}
	fmt.Println("got reader...")

	r := bufio.NewReader(reader)
	msg, err := r.ReadString('\n')
	if err != nil {
		// handle error
		fmt.Printf("error 1: %s or Connection Closed\n", err)
		return err
	}
	fmt.Printf("Received response: %+v", msg)

	return nil
}

func acceptPipeConnections(pipe namedpipe.NamedPipeServer) {
	reqID := "dummyreq"
	fmt.Println("Listening for pipe connections...")
	for {
		err := pipe.NewClient(reqID)
		if err != nil {
			fmt.Printf("Error: %+v", err)
			return
		}

		fmt.Println("New client connected...")

		var errCode error
		for errCode == nil {
			errCode = readData(reqID, pipe)
		}
	}
}

// CreateServer - 서버 시작
func CreateServer() {
	fmt.Println("Attempting to create pipe")
	pipeName := "helloNamedPipe"
	pipe, err := namedpipe.NewNamedPipeServer(pipeName)
	if err != nil {
		fmt.Printf("Error: %+v\n", err)
		os.Exit(1)
	}
	fmt.Println("Created pipe")
	acceptPipeConnections(pipe)
}

func main() {
	CreateServer()
}
