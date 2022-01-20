import React, { useEffect, useState } from 'react';
import { Button, Modal } from 'react-bootstrap';
import DatePicker from "react-datepicker";
import "react-datepicker/dist/react-datepicker.css";

export const CreateWorkout = (props) => {

  const [date, setDate] = useState(new Date());

  useEffect(() => {
    if (!props.workout.date) {
      setDate(new Date());
      return;
    }

    setDate(new Date(props.workout.date));
  }, [props.workout]);

  const saveAndCloseCreateWorkoutModal = () => {
    props.workout.date = date;

    props.saveAndCloseCreateWorkoutModal();
  }

  return (
    <Modal show={props.showCreateWorkoutModal} onHide={props.closeCreateWorkoutModal} dialogClassName="create-workout-modal">
      <Modal.Header>
        <Modal.Title>{props.workout.id ? "Update Workout" : "Add Workout"}</Modal.Title>
      </Modal.Header>
      <Modal.Body>
        <div className="container">
          <form>
            <div className="row">
              <DatePicker
                selected={date}
                onChange={date => setDate(date)}
                dateFormat={'dd/MM/yyyy'}
              />
            </div>
          </form>
        </div>
      </Modal.Body>
      <Modal.Footer>
        <Button variant="secondary" onClick={props.closeCreateWorkoutModal}>
          Cancel
        </Button>
        <Button variant="primary" onClick={saveAndCloseCreateWorkoutModal} disabled={!date}>
          {props.workout.id ? 'Update' : 'Add'}
        </Button>
      </Modal.Footer>
    </Modal>  
  )
}
