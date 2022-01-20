import { faPen, faPlus, faTrash } from '@fortawesome/free-solid-svg-icons';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import moment from 'moment';
import React, { useEffect, useState } from 'react';
import { Button, Modal } from 'react-bootstrap';
import { createProgramExerciseAsync, deleteProgramExerciseAsync, getAllProgramExercisesByProgramIdAsync, updateProgramExerciseAsync } from '../services/ProgramExerciseService';
import { createWorkoutAsync, deleteWorkoutAsync, getAllWorkoutsByProgramIdAsync, updateWorkoutAsync } from '../services/WorkoutService';
import { CreateProgramExercise } from './CreateProgramExercise';
import { CreateWorkout } from './CreateWorkout';
import { ViewWorkout } from './ViewWorkout';

export const ViewProgram = (props) => {

  const [programExercises, setProgramExercises] = useState([]);
  const [workouts, setWorkouts] = useState([]);
  const [showCreateProgramExerciseModal, setShowCreateProgramExerciseModal] = useState(false);
  const [showCreateWorkoutModal, setShowCreateWorkoutModal] = useState(false);
  const [showViewWorkoutModal, setShowViewWorkoutModal] = useState(false);
  const [programExerciseModalData, setProgramExerciseModalData] = useState({});
  const [workoutModalData, setWorkoutModalData] = useState({});

  useEffect(() => {
    if (!props.program.id) return;

    getAllProgramExercisesByProgramIdAsync(props.program.id)
      .then(programExercises => setProgramExercises(programExercises));
    getAllWorkoutsByProgramIdAsync(props.program.id)
      .then(workouts => setWorkouts(workouts));

    return function cleanup() {
      setProgramExercises([]);
      setWorkouts([]);
    };

  }, [props.program]);

  const createOrUpdateProgramExercise = () => {
    let programId = props.program.id;

    if (programExerciseModalData.id) {
      programExerciseModalData.programId = programId;
      updateProgramExerciseAsync(programExerciseModalData)
        .then(response => {
          let index = programExercises.findIndex(programExercise => programExercise.id === response.id);
          programExercises[index] = response;
          setProgramExercises([...programExercises]);
        });
    }
    else {
      programExerciseModalData.programId = programId;
      createProgramExerciseAsync(programExerciseModalData)
        .then(response => {
          programExercises.push(response);
          setProgramExercises([...programExercises]);
        });
    }
  }

  const deleteProgramExercise = (programExerciseToDelete) => {
    if (window.confirm(`Are you sure to want to delete Exercise ${programExerciseToDelete.name}?`)) {
      deleteProgramExerciseAsync(programExerciseToDelete.id)
        .then(() => {
          setProgramExercises(programExercises.filter(programExercise => programExercise.id !== programExerciseToDelete.id));
        });
    }
  }

  const createOrUpdateWorkout = () => {
    let programId = props.program.id;

    if (workoutModalData.id) {
      workoutModalData.programId = programId;
      updateWorkoutAsync(workoutModalData)
        .then(response => {
          let index = workouts.findIndex(workout => workout.id === response.id);
          workouts[index] = response;
          setWorkouts([...workouts]);
        });
    }
    else {
      workoutModalData.programId = programId;
      workoutModalData.date = workoutModalData.date || new Date();
      createWorkoutAsync(workoutModalData)
        .then(response => {
          workouts.push(response);
          setWorkouts([...workouts]);
        });
    }
  }

  const deleteWorkout = (workoutToDelete) => {
    if (window.confirm(`Are you sure to want to delete Workout ${workoutToDelete.id}?`)) {
      deleteWorkoutAsync(workoutToDelete.id)
        .then(() => {
          setWorkouts(workouts.filter(workout => workout.id !== workoutToDelete.id));
        });
    }
  }

  const openCreateProgramExerciseModal = (programExercise) => {
    setShowCreateProgramExerciseModal(true);
    setProgramExerciseModalData(programExercise);
  }

  const closeCreateProgramExerciseModal = () => {
    setShowCreateProgramExerciseModal(false);
    setProgramExerciseModalData({});
  }

  const saveAndCloseCreateProgramExerciseModal = () => {
    createOrUpdateProgramExercise();
    closeCreateProgramExerciseModal();
  }

  const openCreateWorkoutModal = (workout) => {
    setShowCreateWorkoutModal(true);
    setWorkoutModalData(workout);
  }

  const closeCreateWorkoutModal = () => {
    setShowCreateWorkoutModal(false);
    setWorkoutModalData({});
  }

  const saveAndCloseCreateWorkoutModal = () => {
    createOrUpdateWorkout();
    closeCreateWorkoutModal();
  }

  const openViewWorkoutModal = (workout) => {
    setShowViewWorkoutModal(true);
    setWorkoutModalData(workout);
  }

  const closeViewWorkoutModal = () => {
    setShowViewWorkoutModal(false);
    setWorkoutModalData({});
  }

  const ProgramExerciseRow = (programExercise, index) => {
    return (
      <tr key={index}>
        <td>{programExercise.name}</td>
        <td>{programExercise.sets}</td>
        <td>{programExercise.repetitions}</td>
        <td>{programExercise.restPeriod}</td>
        <td className="table-btn-cell">
          <div className="btn-group mr-2" role="group" aria-label="First group">
            <button type="button" onClick={() => openCreateProgramExerciseModal(programExercise)} className="btn btn-success"><FontAwesomeIcon icon={faPen} /></button>
          </div>
          <div className="btn-group mr-2" role="group" aria-label="Second group">
            <button type="button" onClick={() => deleteProgramExercise(programExercise)} className="btn btn-danger"><FontAwesomeIcon icon={faTrash} /></button>
          </div>
        </td>
      </tr>
    )
  }

  const WorkoutRow = (workout, index) => {
    return (
      <tr key={index}>
        <td>{moment(workout.date).format('DD/MM/YYYY')}</td>
        <td className="table-btn-cell">
          <div className="btn-group mr-2" role="group" aria-label="First group">
            <button type="button" onClick={() => openViewWorkoutModal(workout)} className="btn btn-secondary">View</button>
          </div>
          <div className="btn-group mr-2" role="group" aria-label="Second group">
            <button type="button" onClick={() => openCreateWorkoutModal(workout)} className="btn btn-success"><FontAwesomeIcon icon={faPen} /></button>
          </div>
          <div className="btn-group mr-2" role="group" aria-label="Third group">
            <button type="button" onClick={() => deleteWorkout(workout)} className="btn btn-danger"><FontAwesomeIcon icon={faTrash} /></button>
          </div>
        </td>
      </tr>
    )
  }

  const programExerciseTable = programExercises.map((programExercise, index) => ProgramExerciseRow(programExercise, index))
  const workoutTable = workouts.map((workout, index) => WorkoutRow(workout, index))

  return (
    <Modal show={props.showViewProgramModal} onHide={props.closeViewProgramModal} dialogClassName="view-program-modal">
      <Modal.Header>
        <Modal.Title>{props.program.name}</Modal.Title>
      </Modal.Header>
      <Modal.Body>
        <div className="container">
          <h5 className="table-heading">Exercises</h5>
          <button type="button" onClick={() => openCreateProgramExerciseModal({})} className="btn btn-primary btn-create"><FontAwesomeIcon icon={faPlus} /> Add Exercise</button>
          <CreateProgramExercise
            programExercise={programExerciseModalData}
            showCreateProgramExerciseModal={showCreateProgramExerciseModal}
            closeCreateProgramExerciseModal={closeCreateProgramExerciseModal}
            saveAndCloseCreateProgramExerciseModal={saveAndCloseCreateProgramExerciseModal}>
          </CreateProgramExercise>
          <table className="table">
            <thead className="table-header">
              <tr>
                <th>Name</th>
                <th>Sets</th>
                <th>Repetitions</th>
                <th>Rest Period</th>
                <th></th>
              </tr>
            </thead>
            <tbody>
              {programExerciseTable}
            </tbody>
          </table>
        </div>
        {programExercises.length > 0 &&
          <div className="container">
            <h5 className="table-heading">Workouts</h5>
            <button type="button" onClick={() => openCreateWorkoutModal({})} className="btn btn-primary btn-create"><FontAwesomeIcon icon={faPlus} /> Add Workout</button>
            <CreateWorkout
              workout={workoutModalData}
              showCreateWorkoutModal={showCreateWorkoutModal}
              closeCreateWorkoutModal={closeCreateWorkoutModal}
              saveAndCloseCreateWorkoutModal={saveAndCloseCreateWorkoutModal}>
            </CreateWorkout>
            <table className="table">
              <thead className="table-header">
                <tr>
                  <th>Date</th>
                  <th></th>
                </tr>
              </thead>
              <tbody>
                {workoutTable}
                <ViewWorkout
                  workout={workoutModalData}
                  showViewWorkoutModal={showViewWorkoutModal}
                  closeViewWorkoutModal={closeViewWorkoutModal}>
              </ViewWorkout>
              </tbody>
            </table>
          </div>
        }
      </Modal.Body>
      <Modal.Footer>
        <Button variant="secondary" onClick={props.closeViewProgramModal}>
          Close
        </Button>
      </Modal.Footer>
    </Modal>
  )
}
