import { faPen, faPlus, faTrash } from '@fortawesome/free-solid-svg-icons';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import React, { useEffect, useState } from 'react';
import '../custom.css';
import { createProgramAsync, deleteProgramAsync, getAllProgramsAsync, updateProgramAsync } from '../services/ProgramService';
import { CreateProgram } from './CreateProgram';
import { ViewProgram } from './ViewProgram';

export const Programs = () => {

  const [programs, setPrograms] = useState([]);
  const [showCreateProgramModal, setShowCreateProgramModal] = useState(false);
  const [showViewProgramModal, setShowViewProgramModal] = useState(false);
  const [programModalData, setProgramModalData] = useState({});

  useEffect(() => {
    getAllProgramsAsync()
      .then(programs => setPrograms(programs));

    return function cleanup() {
      setPrograms([]);
    };

  }, []);

  const createOrUpdateProgram = () => {
    if (programModalData.id) {
      updateProgramAsync(programModalData)
        .then(response => {
          let index = programs.findIndex(program => program.id === response.id);
          programs[index] = response;
          setPrograms([...programs]);
        });
    }
    else {
      createProgramAsync(programModalData)
        .then(response => {
          programs.push(response);
          setPrograms([...programs]);
        });
    }
  }

  const deleteProgram = (programToDelete) => {
    if (window.confirm(`Are you sure to want to delete Program ${programToDelete.name}?`)) {
      deleteProgramAsync(programToDelete.id)
        .then(() => {
          setPrograms(programs.filter(program => program.id !== programToDelete.id));
        });
    }
  }

  const openCreateProgramModal = (program) => {
    setShowCreateProgramModal(true);
    setProgramModalData(program);
  }

  const closeCreateProgramModal = () => {
    setShowCreateProgramModal(false);
    setProgramModalData({});
  }

  const saveAndCloseCreateProgramModal = () => {
    createOrUpdateProgram();
    closeCreateProgramModal();
  }

  const openViewProgramModal = (program) => {
    setShowViewProgramModal(true);
    setProgramModalData(program);
  }

  const closeViewProgramModal = () => {
    setShowViewProgramModal(false);
    setProgramModalData({});
  }

  const ProgramRow = (program, index) => {
    return (
      <tr key={index}>
        <td>{program.name}</td>
        <td className="table-btn-cell">
          <div className="btn-group mr-2" role="group" aria-label="First group">
            <button type="button" onClick={() => openViewProgramModal(program)} className="btn btn-secondary">View</button>
          </div>
          <div className="btn-group mr-2" role="group" aria-label="Second group">
            <button type="button" onClick={() => openCreateProgramModal(program)} className="btn btn-success"><FontAwesomeIcon icon={faPen} /></button>
          </div>
          <div className="btn-group mr-2" role="group" aria-label="Third group">
            <button type="button" onClick={() => deleteProgram(program)} className="btn btn-danger"><FontAwesomeIcon icon={faTrash} /></button>
          </div>
        </td>
      </tr>
    )
  }

  const programTable = programs.map((program, index) => ProgramRow(program, index))

  return (
    <div className="container">
      <h2 className="table-heading">Programs</h2>
      <button type="button" onClick={() => openCreateProgramModal({})} className="btn btn-primary btn-create"><FontAwesomeIcon icon={faPlus} /> Create Program</button>
      <CreateProgram
        program={programModalData}
        showCreateProgramModal={showCreateProgramModal}
        closeCreateProgramModal={closeCreateProgramModal}
        saveAndCloseCreateProgramModal={saveAndCloseCreateProgramModal}>
      </CreateProgram>
      <table className="table">
        <thead className="table-header">
          <tr>
            <th>Name</th>
            <th></th>
          </tr>
        </thead>
        <tbody>
          {programTable}
          <ViewProgram
            program={programModalData}
            showViewProgramModal={showViewProgramModal}
            closeViewProgramModal={closeViewProgramModal}>
          </ViewProgram>
        </tbody>
      </table>
    </div>
  )
}
